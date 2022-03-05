Component('render-markdown', {
    props: ['modelValue'],
    modules: [
        '/assets/js/marked.js'
    ],
    data() {
        return {
            rendered: null
        }
    },
    created: function () {
        this.rendered = marked.parse(this.$props.modelValue);
    }
});