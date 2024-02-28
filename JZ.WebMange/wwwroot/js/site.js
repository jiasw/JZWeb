// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const MyShower = {
    ShowMsg: function (msg, callback) {
        layer.msg(msg, {
            icon: 1,
            time: 1000 //2秒关闭（如果不配置，默认是3秒）
        }, function () {
            if (callback) {
                callback();
            }
        });

    }


}
