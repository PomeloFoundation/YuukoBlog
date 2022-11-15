var marked = require('/assets/js/marked.js');

Component('render-markdown', {
    props: ['modelValue'],
    data() {
        return {
            rendered: null
        }
    },
    created: function () {
        this.rendered = marked.parse(this.$props.modelValue);
    }
});