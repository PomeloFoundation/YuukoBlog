var PomeloVueOptions = {
    resolveModulesParallelly: false,
    mobile() {
        return false;
    }
};

var PueCQOptions = {
    beforeSend(xhr) {
        if (window.localStorage.getItem('token')) {
            xhr.setRequestHeader('Authorization', 'Token ' + window.localStorage.getItem('token'));
            return;
        }

        if (!window.Pomelo) {
            return;
        }

        if (!Pomelo.root()) {
            return;
        }

        if (Pomelo.root().token) {
            xhr.setRequestHeader('Authorization', 'Token ' + Pomelo.root().token);
            return;
        }
    }
};