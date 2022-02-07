Page({
    layout: '/shared/_master',
    data() {
        return {
            account: null,
            password: null
        };
    },
    created() {
    },
    methods: {
        async save() {
            try {
                await Pomelo.CQ.Post('/api/admin/site', {
                    account: this.account,
                    password: this.password,
                    site: this.$root.info.site,
                    description: this.$root.info.description,
                    name: this.$root.info.name,
                    avatarUrl: this.$root.info.avatarUrl
                });
                this.$root.views.info.refresh();
                alert('Site info updated.');
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