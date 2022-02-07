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
        '/components/render-markdown'
    ],
    modules: [
        '/assets/js/jquery.js',
        '/assets/js/highlight.js'
    ],
    data() {
        return {
            page: null,
            catalog: null,
            tag: null,
            month: null,
            year: null,
            posts: null,
            catalog: null,
            paging: [1]
        };
    },
    async created() {
        if (!this.page) {
            this.page = 1;
        }

        if (this.catalog) {
            this.posts = await Pomelo.CQ.Get('/api/post/catalog/' + this.catalog + '/' + this.page);
        } else if (this.year && this.month) {
            this.posts = await Pomelo.CQ.Get('/api/post/calendar/' + this.year + '/' + this.month + '/' + this.page);
        } else if (this.tag) {
            this.posts = await Pomelo.CQ.Get('/api/post/tag/' + this.tag + '/' + this.page);
        } else {
            this.posts = await Pomelo.CQ.Get('/api/post/page/' + this.page);
        }
        await yield();
        this.updatePagination();
        await yield();
        Highlight();
    },
    methods: {
        generatePageLink(page) {
            if (this.catalog) {
                return '/catalog/' + this.catalog + '/' + page;
            } else if (this.year && this.month) {
                return '/calendar/' + this.year + '/' + this.month + '/' + page;
            } else if (this.tag) {
                return '/tag/' + this.tag + '/' + page;
            } else {
                return '/' + page;
            }
        },
        updatePagination() {
            if (!this.posts) {
                return;
            }

            var current = this.posts.currentPage;
            var begin = current - 2;
            var end = current + 2;
            var max = this.posts.totalPages;
            begin = Math.max(1, begin);
            end = Math.min(end, max);
            this.paging = this.generateArray(begin, end);
        },
        generateArray(begin, end) {
            var arr = [];
            if (begin > end) {
                return arr;
            }

            for (var i = begin; i <= end; ++i) {
                arr.push(i);
            }

            return arr;
        }
    }
});