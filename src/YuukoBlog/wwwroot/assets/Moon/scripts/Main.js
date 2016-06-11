var autoplay = false;

var blog = window.blog = {
    autoHighlight: function () {
        if (window.enable_autoHighlight == undefined) {
            window.enable_autoHighlight = false
        }
        if (!window.enable_autoHighlight) {
            return
        }
        var a = null;
        $(window).scroll(function () {
            var c = $(window).scrollTop();
            var b = $(window).height();
            $(window.enable_autoHighlight).each(function () {
                var d = c + (b * 0.35);
                if (d > $(this).offset().top && d < $(this).offset().top + $(this).height()) {
                    if (a !== null) {
                        a.removeClass("active")
                    }
                    a = $(this).addClass("active");
                    return false
                }
            })
        }).scroll()
    },
    init: function () {
        var a = blog;
        a.autoHighlight();
        MusicPlayerEnabled();
    }
};
var lock = false;

function bor(musicSrc) {
    if (autoplay) {
        var bower = window.navigator.userAgent;
        if (bower.indexOf("MSIE 6") != -1 || bower.indexOf("MSIE 7") != -1 || bower.indexOf("MSIE 8") != -1)
        { return "<embed src='" + musicSrc + "' class='muc' autostart=true loop=true hiddle=true>"; }
        if (bower.indexOf("Firefox") != -1)
        { return "<audio src='" + musicSrc + "' class='muc' controls loop autoplay preload><p>您的浏览器版本过低请升级您的浏览器</p></audio>" }
        else { return "<audio src='" + musicSrc + "' class='muc' controls loop autoplay preload><p>您的浏览器版本过低请升级您的浏览器</p></audio>" }
    }
    else {
        var bower = window.navigator.userAgent;
        if (bower.indexOf("MSIE 6") != -1 || bower.indexOf("MSIE 7") != -1 || bower.indexOf("MSIE 8") != -1)
        { return "<embed src='" + musicSrc + "' class='muc' loop=true hiddle=true>"; }
        if (bower.indexOf("Firefox") != -1)
        { return "<audio src='" + musicSrc + "' class='muc' controls loop preload><p>您的浏览器版本过低请升级您的浏览器</p></audio>" }
        else { return "<audio src='" + musicSrc + "' class='muc' controls loop preload><p>您的浏览器版本过低请升级您的浏览器</p></audio>" }
    }
};

$(document).ready(function () {
    $('#lstTemplate').change(function () {
        window.location = "/home/template?folder=" + $(this).val();
    });
    Highlight();
    $('#btnSearch').click(function () {
        if ($('#txtSearch').val())
            window.location = "/Search/" + encodeURIComponent($('#txtSearch').val());
        else
            window.location = "/";
    });

    $('#txtSearch').keydown(function (e) {
        if (e.keyCode == 13)
        {
            if ($('#txtSearch').val())
                window.location = "/Search/" + encodeURIComponent($('#txtSearch').val());
            else
                window.location = "/";
        }
    });

    blog.init();
    if (typeof (ArticleList) != "undefined")
    {
        LoadArticles();
        $(window).scroll(function () {
            totalheight = parseFloat($(window).height()) + parseFloat($(window).scrollTop());
            if ($(document).height() <= totalheight) {
                LoadArticles();
            }
        });
    }
});

function DropEnable() {
    $('.markdown-textbox').unbind().each(function () {
        $(this).dragDropOrPaste(function (obj) {
            var pos = obj.getCursorPosition();
            var str = obj.val();
            if (pos == 0 && !obj.is(':focus'))
                pos = str.length;
            obj.val(str.substr(0, pos) + '\r\n![Upload](Uploading...)\r\n' + str.substr(pos));
        },
        function (obj, result) {
            var content = obj.val().replace('![Upload](Uploading...)', '![' + result.name + '](/file/download/' + result.id + ')');
            obj.val(content);
        });
    });
}

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