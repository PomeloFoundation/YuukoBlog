function sleep(time) {
    return new Promise(function (res, rej) {
        setTimeout(function () { res(null); }, time);
    });
}

function yield() {
    return sleep(0);
}

var replaceInnerText = '![Upload](Uploading...)';
var replaceText = '\r\n' + replaceInnerText + '\r\n';

Component('simple-mde', {
    props: ['modelValue'],
    modules: [
        '/assets/js/jquery.js',
        '/assets/js/simplemde.js',
        '/assets/js/pomelo.dragdrop.js'
    ],
    data() {
        return {
            id: null,
            editor: null
        }
    },
    methods: {
        guid() {
            function S4() {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            }
            return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
        }
    },
    created: function () {
        this.id = this.guid();
    },
    async mounted() {
        var id = 'simplemde-' + this.id;
        var editor = new SimpleMDE({
            element: document.getElementById(id),
            spellChecker: false,
            status: false
        });
        var self = this;
        editor.codemirror.on('change', function () {
            self.$emit('update:modelValue', editor.value());
        });

        await sleep(500);
        console.log(jQuery('#' + id).length);
        var begin_pos, end_pos;
        jQuery('#' + id).parent().children().dragDropOrPaste(function () {
            begin_pos = editor.codemirror.getCursor();
            editor.codemirror.setSelection(begin_pos, begin_pos);
            editor.codemirror.replaceSelection(replaceText);
            begin_pos.line++;
            end_pos = { line: begin_pos.line, ch: begin_pos.ch + replaceInnerText.length };
        },
            function (result) {
                editor.codemirror.setSelection(begin_pos, end_pos);
                editor.codemirror.replaceSelection('![' + result.fileName + '](/blob/download/' + result.id + ')');
            });
    }
});