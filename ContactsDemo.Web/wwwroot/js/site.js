var utils = (function () {

    function getQSParam(name) {
        return getUrlVars()[name];
    }

    var _siteUrl = "/";

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }

    function dateFormat(value) {
        var dueDateTmp = new Date(value);

        if (dueDateTmp) {
            let date = dueDateTmp.getDate();
            let month = (dueDateTmp.getMonth() + 1);
            let hours = dueDateTmp.getHours();
            let minutes = dueDateTmp.getMinutes();

            if (date < 10) {
                date = '0' + date;
            }

            if (month < 10) {
                month = '0' + month;
            }

            if (hours < 10) {
                hours = '0' + hours;
            }

            if (minutes < 10) {
                minutes = '0' + minutes;
            }

            return date + '.' + month + '.' + dueDateTmp.getFullYear() + " " + hours + ":" + minutes;
        }

        return value;
    }

    return {
        siteUrl: _siteUrl,
        getQSParam: getQSParam,
        dateFormat: dateFormat
    };

})();