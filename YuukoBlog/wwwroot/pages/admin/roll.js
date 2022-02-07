Page({
    layout: '/shared/_master',
    data() {
        return {
            view: null,
            rolls: [],
            add: {
                url: null,
                display: null,
                priority: 999
            }
        };
    },
    created() {
        this.view = Pomelo.CQ.CreateView('/api/statistics/roll', {});
        var self = this;
        this.view.fetch(function (result) {
            self.rolls = result.data;
        });
    },
    methods: {
        async remove(id) {
            if (confirm('Are you sure you want to delete this blog roll?')) {
                await Pomelo.CQ.Delete('/api/roll/' + id);
                this.view.refresh();
                this.$root.views.roll.refresh();
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
            await Pomelo.CQ.Post('/api/roll', this.add);
            this.view.refresh();
            this.$root.views.roll.refresh();
        },
        async update(roll) {
            await Pomelo.CQ.Patch('/api/roll/' + roll.id, roll);
            this.view.refresh();
            this.$root.views.roll.refresh();
            this.reset();
        }
    }
});