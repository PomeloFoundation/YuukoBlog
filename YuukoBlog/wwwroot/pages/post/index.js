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
    modules: [
        '/assets/js/jquery.js',
        '/assets/js/highlight.js'
    ],
    components: [
        '/components/render-markdown',
        '/components/simple-mde'
    ],
    data() {
        return {
            id: null,
            post: null,
            views: {
                post: null,
                comment: null
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
    created() {
        this.$root.isPost = true;
    },
    async mounted() {
        window.test = this;
        this.views.post = (await Pomelo.CQ.CreateView('/api/post/' + this.id));
        var self = this;
        this.views.post.fetch(function (result) {
            self.post = result.data;
            if (self.post.catalogId) {
                self.$root.catalog = self.post.catalog.url;
            }
        });
        this.loadComments();
    },
    unmounted: function () {
        this.$root.isPost = false;
    },
    methods: {
        moment(str) {
            return this.$root.moment(str);
        },
        reply(id) {
            this.newComment.parentId = id;
        },
        async deleteComment(id) {
            if (confirm("Are you sure to remove this comment?")) {
                await Pomelo.CQ.Delete('/api/comment/' + id);
                this.views.comment.refresh();
            }
        },
        async deletePost() {
            if (confirm("Are you sure you want to remove this post?")) {
                await Pomelo.CQ.Delete('/api/post/' + this.post.id);
                Pomelo.Redirect('/');
            }
        },
        async loadComments() {
            var self = this;
            this.views.comment = Pomelo.CQ.CreateView('/api/comment/' + this.id, {});
            this.views.comment.fetch(async function (result) {
                self.comments = result.data;
                await yield();
                Highlight();
            });
        },
        async postComment() {
            var self = this;
            if (!this.$root.token) {
                if (!this.newComment.name) {
                    alert('Please input your name');
                    return;
                }
                if (!this.newComment.email) {
                    alert('Please input your email');
                    return;
                }
            }
            if (!this.newComment.content) {
                alert('Please input content');
                return;
            }

            try {
                await Pomelo.CQ.Post('/api/comment/' + this.id, this.newComment);
            } catch (ex) {
            }
            this.views.comment.refresh();
            this.newComment.name = null;
            this.newComment.email = null;
            this.newComment.content = '';
            var tmp = this.newComment.parentId;
            this.newComment.parentId = null;
            await yield();
            this.newComment.parentId = tmp;
        }
    }
});