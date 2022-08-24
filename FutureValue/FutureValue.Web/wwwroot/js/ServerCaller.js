var Options =  (function () {
    function Options(url, method, data) {
        this.url = url;
        this.method = method || "get";
        this.data = data || {};
    }
    return Options;
}());
var Service = (function () {
    function Service() {
        var _this = this;
        this.request = function (options, successCallback, errorCallback) {
            var that = _this;
            var args = options.method === "get" ? {
                method: options.method,
                headers: { "Content-Type": "application/json; charset=utf-8" }
            } : {
                method: options.method,
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(options.data)
            };
            fetch(options.url, args)
                .then(function (res) { return res.json(); }) // parse response as JSON (can be res.text() for plain response)
                .then(function (response) {
                    successCallback(response);
                })
                .catch(function (err) {
                    errorCallback(err);
                });
        };
        this.get = function (url, successCallback, errorCallback) {
            _this.request(new Options(url), successCallback, errorCallback);
        };
        this.getWithDataInput = function (url, data, successCallback, errorCallback) {
            _this.request(new Options(url, "get", data), successCallback, errorCallback);
        };
        this.post = function (url, successCallback, errorCallback) {
            _this.request(new Options(url, "post"), successCallback, errorCallback);
        };
        this.postWithData = function (url, data, successCallback, errorCallback) {
            _this.request(new Options(url, "post", data), successCallback, errorCallback);
        };
    }
    return Service;
}());