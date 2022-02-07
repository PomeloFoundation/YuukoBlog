Page({
    layout: '/shared/_master',
    data() {
        return {
            view: null,
            links: [],
            add: {
                url: null,
                display: null,
                icon: null,
                priority: 999
            }
        };
    },
    created() {
        this.view = Pomelo.CQ.CreateView('/api/statistics/link', {});
        var self = this;
        this.view.fetch(function (result) {
            self.links = result.data;
        });
    },
    methods: {
        async remove(id) {
            if (confirm('Are you sure you want to delete this link?')) {
                await Pomelo.CQ.Delete('/api/link/' + id);
                this.view.refresh();
                this.$root.views.link.refresh();
            }
        },
        reset() {
            this.add = {
                url: null,
                display: null,
                icon: null,
                priority: 999
            };
        },
        async create() {
            await Pomelo.CQ.Post('/api/link', this.add);
            this.view.refresh();
            this.$root.views.link.refresh();
            this.reset();
        },
        async update(link) {
            await Pomelo.CQ.Patch('/api/link/' + link.id, link);
            this.view.refresh();
            this.$root.views.link.refresh();
        }
    }
});