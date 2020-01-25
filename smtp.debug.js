Email = {
    send: function (from, to, subject, body, host, username, password) {        
        var nocache = Math.floor((Math.random() * 1000000) + 1);
        var strUrl = "https://smtpjs.com/smtp.aspx?";
        strUrl += "From=" + from;
        strUrl += "&to=" + to;
        strUrl += "&Subject=" + encodeURIComponent(subject);
        strUrl += "&Body=" + encodeURIComponent(body);
        if (host.token == undefined) {           
            strUrl += "&Host=" + host;
            strUrl += "&Username=" + username;
            strUrl += "&Password=" + password;
            strUrl += "&Action=Send";
        }
        else {
            strUrl += "&SecureToken=" + host.token; 
            strUrl += "&Action=SendFromStored";
        }
        strUrl += "&cachebuster=" + nocache;
        Email.ajax(strUrl);
    },
    sendWithAttachment: function (from, to, subject, body, host, username, password, attachment) {
        var nocache = Math.floor((Math.random() * 1000000) + 1);
        var strUrl = "https://smtpjs.com/smtp.aspx?";
        strUrl += "From=" + from;
        strUrl += "&to=" + to;
        strUrl += "&Subject=" + encodeURIComponent(subject);
        strUrl += "&Body=" + encodeURIComponent(body);
        strUrl += "&Attachment=" + encodeURIComponent(attachment);
        if (host.token == undefined) {
            strUrl += "&Host=" + host;
            strUrl += "&Username=" + username;
            strUrl += "&Password=" + password;
            strUrl += "&Action=Send";
        }
        else {
            strUrl += "&SecureToken=" + host.token;
            strUrl += "&Action=SendFromStored";
        }
        strUrl += "&cachebuster=" + nocache;
        Email.ajax(strUrl);
    },
    ajax: function (src) {
        var xhr = Email.createCORSRequest('GET', src);
        xhr.onload = function () {
            var responseText = xhr.responseText;
            console.log(responseText);
        };
        xhr.send();
    },
    createCORSRequest: function (method, url) {
        var xhr = new XMLHttpRequest();
        if ("withCredentials" in xhr) {
            xhr.open(method, url, true);
        } else if (typeof XDomainRequest != "undefined") {
            xhr = new XDomainRequest();
            xhr.open(method, url);
        } else {
            xhr = null;
        }
        return xhr;
    }
};