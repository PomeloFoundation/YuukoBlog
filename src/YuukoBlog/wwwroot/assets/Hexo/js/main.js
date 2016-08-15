require([], function () {

    var isMobileInit = false;
    var loadMobile = function () {
        require(['/assets/Hexo/js/mobile.js'], function (mobile) {
            mobile.init();
            isMobileInit = true;
        });
    }
    var isPCInit = false;
    var loadPC = function () {
        require(['/assets/Hexo/js/pc.js'], function (pc) {
            pc.init();
            isPCInit = true;
        });
    }

    var browser = {
        versions: function () {
            var u = window.navigator.userAgent;
            return {
                trident: u.indexOf('Trident') > -1, //IE内核
                presto: u.indexOf('Presto') > -1, //opera内核
                webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
                gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
                mobile: !!u.match(/AppleWebKit.*Mobile.*/), //是否为移动终端
                ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器
                iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者安卓QQ浏览器
                iPad: u.indexOf('iPad') > -1, //是否为iPad
                webApp: u.indexOf('Safari') == -1,//是否为web应用程序，没有头部与底部
                weixin: u.indexOf('MicroMessenger') == -1 //是否为微信浏览器
            };
        }()
    }

    $(window).bind("resize", function () {
        if (isMobileInit && isPCInit) {
            $(window).unbind("resize");
            return;
        }
        var w = $(window).width();
        if (w >= 700) {
            loadPC();
        } else {
            loadMobile();
        }
    });

    if (browser.versions.mobile === true || $(window).width() < 700) {
        loadMobile();
    } else {
        loadPC();
    }

    //是否使用fancybox
    if (yiliaConfig.fancybox === true) {
        require(['/assets/Hexo/js/jquery.fancybox.js'], function (pc) {
            var isFancy = $(".isFancy");
            if (isFancy.length != 0) {
                var imgArr = $(".article-inner img");
                for (var i = 0, len = imgArr.length; i < len; i++) {
                    var src = imgArr.eq(i).attr("src");
                    var title = imgArr.eq(i).attr("alt");
                    imgArr.eq(i).replaceWith("<a href='" + src + "' title='" + title + "' rel='fancy-group' class='fancy-ctn fancybox'><img src='" + src + "' title='" + title + "'></a>");
                }
                $(".article-inner .fancy-ctn").fancybox();
            }
        });

    }
    //是否开启动画
    if (yiliaConfig.animate === true) {

        require(['/assets/Hexo/js/jquery.lazyload.js'], function () {
            //avatar
            $(".js-avatar").attr("src", $(".js-avatar").attr("lazy-src"));
            $(".js-avatar")[0].onload = function () {
                $(".js-avatar").addClass("show");
            }
        });

        if (yiliaConfig.isHome === true) {
            //content
            function showArticle() {
                $(".article").each(function () {
                    if ($(this).offset().top <= $(window).scrollTop() + $(window).height() && !($(this).hasClass('show'))) {
                        $(this).removeClass("hidden").addClass("show");
                        $(this).addClass("is-hiddened");
                    } else {
                        if (!$(this).hasClass("is-hiddened")) {
                            $(this).addClass("hidden");
                        }
                    }
                });
            }
            $(window).on('scroll', function () {
                showArticle();
            });
            showArticle();
        }

    }

    //是否新窗口打开链接
    if (yiliaConfig.open_in_new == true) {
        $(".article a[href]").attr("target", "_blank")
    }

});

$(document).ready(function () {
    // Binding blog roll
    $('.sidebar-blog-roll').hover(function () {
        var name = $(this).find('img').attr('alt');
        if (!name)
            return;
        var pos = $(this).position();
        var top = pos.top + $(this).outerHeight();
        var left = pos.left + $(this).outerWidth() / 2;
        $('.sidebar-blog-roll-tip').css('top', top);
        $('.sidebar-blog-roll-tip').css('left', left);
        $('.sidebar-blog-roll-tip').text(name);
        $('.sidebar-blog-roll-tip').addClass('active');
    }, function () {
        $('.sidebar-blog-roll-tip').removeClass('active');
    });

    $('#lstTemplate').change(function () {
        window.location = "/home/template?folder=" + $(this).val();
    });

    Highlight();
});

function savePost(url) {
    $.post('/admin/post/edit', {
        __RequestVerificationToken: $('#frmSavePost input[name="__RequestVerificationToken"]').val(),
        title: $('#txtTitle').val(),
        id: url,
        newId: $('#txtUrl').val(),
        content: $('#txtContent').val(),
        tags: $('#txtTags').val(),
        catalog: $('#lstCatalogs').val(),
        isPage: $('#chkIsPage').is(':checked')
    }, function (html) {
        $('.post-body').html(html);
        $('.post-edit').slideUp();
        $('.post-body').slideDown();
        Highlight();
        popResult('文章保存成功');
    });
}

function editCatalog(id) {
    var parent = $('tr[data-catalog="' + id + '"]');
    parent.find('.display').hide();
    parent.find('.editing').fadeIn();
}

function cancelEditCatalog() {
    $('.editing').hide();
    $('.display').fadeIn();
}

function deleteCatalog(id) {
    var parent = $('tr[data-catalog="' + id + '"]');
    parent.remove();
    $.post('/admin/catalog/delete', {
        id: id,
        __RequestVerificationToken: $('#frmDeleteCatalog input[name="__RequestVerificationToken"]').val()
    }, function () {
        popResult('删除成功');
    });
}

function saveCatalog(id) {
    var parent = $('tr[data-catalog="' + id + '"]');
    $.post('/admin/catalog/edit/', {
        id: id,
        __RequestVerificationToken: $('#frmEditCatalog input[name="__RequestVerificationToken"]').val(),
        newId: parent.find('.title').val(),
        title: parent.find('.title-zh').val(),
        order: parent.find('.order').val()
    }, function () {
        parent.find('.d-title').html(parent.find('.title').val());
        parent.find('.d-title-zh').html(parent.find('.title-zh').val());
        parent.find('.d-order').html(parent.find('.order').val());
        parent.find('.editing').hide();
        parent.find('.display').fadeIn();
        popResult('修改成功');
    });
}

function saveConfig() {
    $.post('/admin/index', $('#frmConfig').serialize(), function () {
        popResult('网站配置信息修改成功');
    });
}

function popResult(txt) {
    var msg = $('<div class="msg hide">' + txt + '</div>');
    msg.css('left', '50%');
    $('body').append(msg);
    msg.css('margin-left', '-' + parseInt(msg.outerWidth() / 2) + 'px');
    msg.removeClass('hide');
    setTimeout(function () {
        msg.addClass('hide');
        setTimeout(function () {
            msg.remove();
        }, 400);
    }, 2600);
}

function DropEnable() {
    $('.markdown-textbox').unbind().each(function () {
        var editor = $(this);
        $(this).dragDropOrPaste(function () {
            var pos = editor.getCursorPosition();
            var str = editor.val();
            if (pos == 0 && !editor.is(':focus'))
                pos = str.length;
            editor.val(str.substr(0, pos) + '\r\n![Upload](Uploading...)\r\n' + str.substr(pos));
        },
        function (result) {
            var content = editor.val().replace('![Upload](Uploading...)', '![' + result.FileName + '](/file/download/' + result.Id + ')');
            editor.val(content);
        });
    });
}
