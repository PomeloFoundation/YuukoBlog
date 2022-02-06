function sleep(time) {
    return new Promise(function (res, rej) {
        setTimeout(function () { res(null); }, time);
    });
}

function yield() {
    return sleep(0);
}

Page({
    layout: '/shared/_master',
    components: [
        '/components/simple-mde'
    ],
    data() {
        return {
            id: null,
            post: null,
            views: {
                post: null
            },
            comments: [],
            newComment: {
                name: null,
                email: null,
                content: '',
                parentId: null
            }
        };
    },
    async mounted() {
        window.test = this;
        this.views.post = (await Pomelo.CQ.CreateView('/api/post/' + this.id));
        var self = this;
        this.views.post.fetch(function (result) {
            self.post = result.data;
        });
        this.loadComments();
    },
    methods: {
        moment(str) {
            return this.$root.moment(str);
        },
        reset: function () {
            this.views.post.refresh();
        },
        async update() {
            await Pomelo.CQ.Patch('/api/post/' + this.post.id, this.post);
            this.views.post.refresh();
            alert('Post has been saved successfully');
        },
        async remove() {
            if (confirm("Are you sure you want to remove this post?")) {
                await Pomelo.CQ.Delete('/api/post/' + this.id);
                Pomelo.Redirect('/');
            }
        }
    }
});