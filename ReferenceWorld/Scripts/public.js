function convertStrToDate(str) {
    if (str == undefined || str.length <= 0) {
        return null;
    }
    else {
        var arr = str.match(/\d+/g);
        if (arr != null && arr.length >= 3) {
            var date = new Date(arr[0], arr[1], arr[2]);
            switch (arr.length) {
                case 4:
                    date.setHours(arr[3]);
                    break;
                case 5:
                    date.setHours(arr[3]);
                    date.setMinutes(arr[4]);
                    break;
                case 6:
                    date.setHours(arr[3]);
                    date.setMinutes(arr[4]);
                    date.setSeconds(arr[5]);
                    break;
            }
            return date;
        }
        else
            return null;
    }
}

function trimString(str) { 
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

function isEmail(str) {
    var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((.[a-zA-Z0-9_-]{2,3}){1,2})$/;
    return reg.test(str);
}