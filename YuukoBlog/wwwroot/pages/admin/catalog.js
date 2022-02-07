Page({
    layout: '/shared/_master',
    data() {
        return {
            _view: null,
            catalogs: [],
            add: {
                url: null,
                title: null,
                icon: null,
                priority: 999
            },
            view: null
        };
    },
    created() {
        var self = this;
        this.view = Pomelo.CQ.CreateView('/api/statistics/catalog', {}, 1000 * 60 * 30)
        this.view.fetch(result => {
            self.catalogs = result.data;
        });
    },
    methods: {
        async remove(id) {
            if (confirm('Are you sure you want to delete this catalog?')) {
                try {
                    await Pomelo.CQ.Delete(`/api/catalog/${id}`)
                    this.view.refresh();
                    this.$root.views.catalog.refresh();
                } catch (err) {
                    if (err && err.message) {
                        alert(err.message);
                    } else {
                        alert(err);
                    }
                }
            }
        },
        reset() {
            this.add = {
                url: null,
                title: null,
                icon: null,
                priority: 999
            };
        },
        async create() {
            try {
                await Pomelo.CQ.Post(`/api/catalog`, this.add);
                this.view.refresh();
                this.$root.views.catalog.refresh();
                this.reset();
            } catch (err) {
                if (err && err.message) {
                    alert(err.message);
                } else {
                    alert(err);
                }
            }
        },
        async update(catalog) {
            try {
                await Pomelo.CQ.Patch(`/api/catalog/${catalog.id}`, catalog);
                this.view.refresh();
                this.$root.views.catalog.refresh();
            } catch (err) {
                if (err && err.message) {
                    alert(err.message);
                } else {
                    alert(err);
                }
            }
        }
    }
});