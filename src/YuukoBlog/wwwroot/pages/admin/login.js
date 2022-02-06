Page({
    layout: '/shared/_master',
    data() {
        return {
            username: null,
            password: null
        };
    },
    methods: {
        async login() {
            try {
                var result = await Pomelo.CQ.Post('/api/admin/session', {
                    username: this.username,
                    password: this.password
                });
                this.$root.token = result.data.token;
                window.localStorage.setItem('token', result.data.token);
                Pomelo.Redirect('/admin');
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