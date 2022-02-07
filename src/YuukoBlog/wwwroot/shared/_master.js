Layout({
    modules: [
        '/assets/js/moment.js'
    ],
    data() {
        return {
            loading: true,
            id: null,
            catalog: null,
            isPost: false,
            isAdmin: false,
            catalogs: [],
            calendars: [],
            tags: [],
            rolls: [],
            links: [],
            info: {},
            token: window.localStorage.getItem('token'),
            views: {
                info: null,
                roll: null,
                catalog: null,
                calendar: null,
                tag: null,
                link: null
            }
        };
    },
    async created() {
        this.loading = false;
        var self = this;
        this.views.info = Pomelo.CQ.CreateView('/api/statistics/info', {}, 1000 * 60 * 30);
        this.views.info.fetch(function (result) {
            self.info = result.data;
        });

        this.views.roll = Pomelo.CQ.CreateView('/api/statistics/roll', {}, 1000 * 60 * 30);
        this.views.roll.fetch(function (result) {
            self.rolls = result.data;
        });

        this.views.catalog = Pomelo.CQ.CreateView('/api/statistics/catalog', {}, 1000 * 60 * 30);
        this.views.catalog.fetch(function (result) {
            self.catalogs = result.data;
        });
            
        this.views.calendar = Pomelo.CQ.CreateView('/api/statistics/calendar', {}, 1000 * 60 * 30);
        this.views.calendar.fetch(function (result) {
            self.calendars = result.data;
        });

        this.views.tag = Pomelo.CQ.CreateView('/api/statistics/tag', {}, 1000 * 60 * 30);
        this.views.tag.fetch(function (result) {
            self.tags = result.data;
        });

        this.views.link = Pomelo.CQ.CreateView('/api/statistics/link', {}, 1000 * 60 * 30);
        this.views.link.fetch(function (result) {
            self.links = result.data;
        });

        if (this.token) {
            if (!(await Pomelo.CQ.Get('/api/admin/session')).data) {
                this.token = null;
                this.logout();
            }
        }
    },
    methods: {
        moment(str) {
            return moment(str);
        },
        logout() {
            window.localStorage.removeItem('token');
            this.token = null;
            Pomelo.Redirect('/');
        },
        async newPost() {
            var post = (await Pomelo.CQ.Post('/api/post', {})).data;
            Pomelo.Redirect('/edit/' + post.url);
        }
    }
});