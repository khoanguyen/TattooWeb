app.service('appUrl', ["$window", AppUrlService]);

function AppUrlService($window) {
    var self = this;
    self.baseUrl = window.tattooWeb.baseUrl;

    // private func
    function resolveFunc(baseUrl) {
        return function (relative) { return self.resolve(baseUrl, relative); };
    }

    self.combine = function (baseUrl, relativeUrl) {
        return baseUrl.replace(/\/+$/, '') + "/" + relativeUrl.replace(/^\/+/, '');
    };

    self.resolve = function (baseUrl, relativeUrl) {
        if (!relativeUrl) return baseUrl;
        return self.combine(baseUrl, relativeUrl);
    }

    self.absoluteOf = resolveFunc(self.baseUrl);
    self.service = resolveFunc(self.combine(self.baseUrl, "home"));

};