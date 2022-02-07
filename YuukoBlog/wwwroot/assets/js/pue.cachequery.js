if (window.Pomelo) {
    Pomelo.CQ = {};
}

var PomeloCQ = (function (exports) {
    if (exports.get) {
        return exports;
    }

    function _combineObject(src, dest) {
        if (!src) {
            return;
        }

        var fields = Object.getOwnPropertyNames(src);
        for (var i = 0; i < fields.length; ++i) {
            dest[fields[i]] = src[fields[i]];
        }
    };

    var _options = {
        isPagedResult(result) {
            if (result.totalRecords == undefined || result.totalPages === undefined || result.currentPage === undefined || result.pageSize === undefined)
                return false;
            else
                return true;
        },
        beforeSend: function (xhr) {
        }
    };
    _combineObject(window.PueCQOptions || {}, _options);

    function clone(x) {
        var json = JSON.stringify(x);
        return JSON.parse(json);
    }

    function getFields(obj) {
        var ret = [];
        if (!obj || typeof obj !== 'object') return ret;

        for (var x in obj) {
            if (obj[x])
                ret.push(x);
        }
        return ret;
    }

    var __cache = {};
    var __cacheDictionary = {};
    var __cacheExpire = {};
    var __cacheFilters = {};
    var __cacheSubscribe = {};

    function SetOptions(options) {
        _combineObject(options, _options);
    }

    function _toUrlString(params, questionMark = true, ignore = null) {
        var keys = Object.keys(params).sort();
        if (!keys.length)
            return '';
        var ret = questionMark ? '?' : '';
        for (var i = 0; i < keys.length; i++) {
            if (ignore) {
                if (ignore.some(x => x == keys[i])) {
                    continue;
                }
            }
            ret += keys[i] + '=' + encodeURI(params[keys[i]]) + '&';
        }
        return ret.substr(0, ret.length - 1);
    };

    function _urlStringToObject(str) {
        str = str.substr(str.indexOf('?') + 1);
        var splitedKeyValuePairs = str.split('&');
        var ret = {};
        for (var i = 0; i < splitedKeyValuePairs.length; i++) {
            var splitedKeyValuePair = splitedKeyValuePairs[i].split('=');
            ret[splitedKeyValuePair[0]] = decodeURI(splitedKeyValuePair[1]);
        }
        return ret;
    };

    function _generateCacheKey(endpoint, params, isPaged) {
        var par = clone(params);
        if (isPaged && par.page) delete par.page;
        return endpoint + _toUrlString(par);
    };

    function _isPagedResult(result) {
        return _options.isPagedResult(result);
    };

    function _xhrRequest(options) {
        var xhr = new XMLHttpRequest();
        xhr.open(options.type, options.url);
        xhr.setRequestHeader('Content-Type', options.contentType);
        if (options.beforeSend) {
            options.beforeSend(xhr);
        }

        if (typeof options.data !== 'string') {
            if (options.contentType.toLocaleLowerCase() == 'application/json') {
                options.data = JSON.stringify(options.data);
            } else {
                options.data = _toUrlString(options.data, false);
            }
        }

        xhr.send(options.data);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4) {
                var ret = options.dataType == 'json' ? JSON.parse(xhr.responseText) : xhr.responseText;
                if (xhr.status >= 200 && xhr.status < 300) {
                    options.success(ret, xhr);
                } else {
                    options.error(ret, xhr);
                }
            }
        };
    };

    function request(endpoint, method, params, dataType, contentType) {
        var self = this;
        return new Promise(function (resolve, reject) {
            _xhrRequest({
                url: endpoint,
                type: method,
                dataType: dataType || 'json',
                contentType: contentType || 'application/json',
                data: method == 'GET' ? null : params,
                success: function (ret) {
                    resolve(ret);
                },
                error: function (err) {
                    reject(err);
                },
                beforeSend: function (xhr) {
                    _options.beforeSend(xhr);
                }
            });
        });
    }

    function get(endpoint, params, dataType) {
        return request(endpoint, 'GET', params, dataType);
    };

    function post(endpoint, params, dataType) {
        return request(endpoint, 'POST', params, dataType);
    };

    function patch(endpoint, params, dataType) {
        return request(endpoint, 'PATCH', params, dataType);
    };

    function put(endpoint, params, dataType) {
        return request(endpoint, 'PUT', params, dataType);
    };

    function _delete(endpoint, params, dataType) {
        return request(endpoint, 'DELETE', params, dataType);
    };

    function removeCache(endpoint, params) {
        var fields = getFields(params);
        var key = _generateCacheKey(endpoint, params, fields.some(x => x === 'page'));
        if (__cache[key]) {
            delete __cache[key];
        }
    };

    function cache(endpoint, params, result, expire) {
        var key;
        if (!__cacheDictionary[endpoint])
            __cacheDictionary[endpoint] = [];
        var isPagedResult = _isPagedResult(result);
        if (!isPagedResult) {
            key = _generateCacheKey(endpoint, params);
            __cache[key] = result;
        } else {
            key = _generateCacheKey(endpoint, params, true);
            if (!__cache[key]) __cache[key] = { isPaged: true };
            __cache[key][result.data.current] = result;
        }
        if (!__cacheDictionary[endpoint].some(x => x == key))
            __cacheDictionary[endpoint].push(key);

        if (expire)
            __cacheExpire[key] = new Date().getTime() + expire;
    };

    function update(method, endpoint, item) {
        var filterFunc;
        if (__cacheFilters[endpoint]) {
            filterFunc = __cacheFilters[endpoint];
        }

        for (var i = 0; i < __cacheDictionary[endpoint].length; i++) {
            var cacheKey = __cacheDictionary[endpoint][i];
            if (filterFunc === undefined) {
                filterFunc = function (data) {
                    var id = data.item.Id;
                    var items = __cache[data.key].filter(x => x.Id == id);
                    if (items.length)
                        return __cache[data.key].indexOf(items[0]);
                    else return -1;
                };
            }
            var isPaged = __cache[cacheKey].isPaged !== undefined;
            var pos = filterFunc({
                isPaged: isPaged,
                method: method,
                endpoint: endpoint,
                item: item,
                key: cacheKey,
                params: _urlStringToObject(cacheKey)
            });

            var reactToChanges = false;
            switch (method) {
                case 'PUT':
                    if (!isPaged) {
                        if (pos >= 0) {
                            __cache[cacheKey].splice(pos, 0, item);
                            reactToChanges = true;
                        }
                    } else {
                        if (pos[0] > 0) {
                            __cache[cacheKey][pos[0]].splice(pos[1], 0, item);
                            reactToChanges = true;
                        }
                    }
                    break;
                case 'PATCH':
                    if (!isPaged) {
                        pos = pos < 0 ? __cache[cacheKey].length : pos;
                        __cache[cacheKey].splice(pos, 1);
                        __cache[cacheKey].splice(pos, 0, item);
                        reactToChanges = true;
                    } else {
                        if (pos[0] >= 0) {
                            pos[1] = pos[1] < 0 ? __cache[cacheKey].length : pos[1];
                            __cache[cacheKey][pos[0]].splice(pos[1], 1);
                            __cache[cacheKey][pos[0]].splice(pos[1], 0, item);
                            reactToChanges = true;
                        }
                    }
                    break;
                case 'DELETE':
                    if (!isPaged) {
                        if (pos >= 0)
                            __cache[cacheKey].splice(pos, 1);
                        reactToChanges = true;
                    } else {
                        if (pos[0] >= 0 && pos[1] >= 0)
                            __cache[cacheKey][pos[0]].splice(pos[1], 1);
                        reactToChanges = true;
                    }
                    break;
                default: break;
            }

            if (reactToChanges) {
                var functions = __cacheSubscribe[cacheKey];
                if (functions && functions.length) {
                    for (var i = 0; i < functions.length; i++) {
                        try {
                            functions[i](isPaged ? __cache[cacheKey][pos[0]] : __cache[cacheKey]);
                        } catch (ex) {
                            functions[i] = null;
                        }
                    }

                    __cacheSubscribe[cacheKey] = __cacheSubscribe[cacheKey].filter(x => x);
                }
            }
        }
    };

    function addFilter(endpoint, func) {
        if (__cacheFilters === undefined)
            __cacheFilters = {};
        __cacheFilters[endpoint] = func;
    };

    function subscribe(key, func) {
        let current = LazyRouting.GetCurrentComponent();
        let uid = current._uid;
        if (current) {
            func = function () {
                let com = LazyRouting.GetCurrentComponent();
                if (com && com._uid != uid) {
                    return;
                } else {
                    func();
                }
            }
        }
        if (__cacheSubscribe[key] === undefined)
            __cacheSubscribe[key] = [];
        __cacheSubscribe[key].push(func);
    };

    function createView(endpoint, params, interval) {
        if (!params) params = {};
        var ret = {
            bindings: [],
            _subscribe: null,
            _fetchFunc: null,
            __cacheInfo: {
                endpoint: endpoint,
                params: params
            },
            removeCache: function () {
                removeCache(__cacheInfo.endpoint, __cacheInfo.params);
            },
            fetch: function (func) {
                if (this._fetchFunc) {
                    var tmp = this._fetchFunc;
                    this._fetchFunc = function (result) {
                        tmp(result);
                        func(result);
                    };
                } else {
                    this._fetchFunc = func;
                }
                var page = params.page;
                var key = _generateCacheKey(endpoint, params, true);
                if (!__cache[key] || (page !== undefined && !__cache[key][page || 1])) {
                    return get(endpoint, params)
                        .then((result) => {
                            cache(endpoint, params, result, interval);
                            try {
                                if (func) {
                                    func(result);
                                }
                            } catch (err) { console.error(err); }
                        });
                } else {
                    if (__cache[key].isPaged) {
                        func(__cache[key][page || 1]);
                        return Promise.resolve(__cache[key][page || 1]);
                    } else {
                        func(__cache[key]);
                        return Promise.resolve(__cache[key]);
                    }
                }
            },
            subscribe: function (type, id, func) {
            },
            unsubscribe: function () {
            },
            refresh: function () {
                removeCache(this.__cacheInfo.endpoint, this.__cacheInfo.params);
                this.fetch(this._fetchFunc);
            }
        };

        if (interval) {
            setInterval(function () {
                ret.refresh();
            }, interval);
        }

        return ret;
    };

    function reset() {
        __cache = {};
        __cacheDictionary = {};
        __cacheExpire = {};
        __cacheFilters = {};
        __cacheSubscribe = {};
    }

    exports.GenerateQueryStringFromObject = _toUrlString;
    exports.GenerateObjectFromQueryString = _urlStringToObject;
    exports.XhrRequest = _xhrRequest;
    exports.Request = request;
    exports.Get = get;
    exports.Post = post;
    exports.Put = put;
    exports.Patch = patch;
    exports.Delete = _delete;
    exports.SetOptions = SetOptions;
    exports.CreateView = createView;
    exports.Subscribe = subscribe;
    exports.Update = update;
    exports.Reset = reset;

    return exports;

})(window.Pomelo ? window.Pomelo.CQ : {});