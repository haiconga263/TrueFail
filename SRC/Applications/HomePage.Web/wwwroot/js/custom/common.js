
const CK_PROC_CART = 'procart';
var count = 0;
function loadCart() {
    var strPros = $.cookie(CK_PROC_CART);
    if (typeof strPros === 'undefined' || strPros == '') {
        strPros = '[]';
    }

    var pros = JSON.parse(strPros);
    $('.badge').html('');
    $('.badge').append(pros.length);
}

function removeProduct(id) {
    loadCart();
}

function addProduct(id) {
    var strPros = $.cookie(CK_PROC_CART);
    if (typeof strPros === 'undefined' || strPros == '') {
        strPros = '[]';
    }

    var pros = JSON.parse(strPros);
    if (typeof pros === 'undefined') {
        pros = [];
    }

    if (typeof id === 'undefined') {
        return;
    }

    var product = {};
    var prostr = $('#pro-' + id).attr('data-pro');
    var tmp = JSON.parse(prostr);
    product.name = tmp.Name;
    product.Id = tmp.Id;
    product.price = tmp.SellingCurrentPrice;
    product.image = tmp.ImageURL;
    product.qty = $('#pro-' + id + ' .pro-qty').val();

    for (var i = 0; i < pros.length; i++) {
        if (pros[i].Id == id) {
            var html = '<div class="alert_page">' +
                '	<div class="alert alert-danger">' +
                '		<a href="#" class="close" data-dismiss="alert" aria-label="close" title="close">×</a>' +
                '		<strong>Đã tồn tại trong giỏ hàng</strong>' +
                '	</div>' +
                '</div>';
            $('#alert_addcart').append(html);
            return;
        }
    }

    pros.push(product);
    $.cookie(CK_PROC_CART, JSON.stringify(pros), { path: '/' });
    
    var html = '<div class="alert_page">' +
        '	<div class="alert alert-success">' +
        '		<a href="#" class="close" data-dismiss="alert" aria-label="close" title="close">×</a>' +
        '		<strong>Thêm thành công</strong>' +
        '	</div>' +
        '</div>';
    $('#alert_addcart').append(html);
    loadCart();
}

function removeProduct(id) {
    var strPros = $.cookie(CK_PROC_CART);
    if (typeof strPros === 'undefined' || strPros == '') {
        strPros = '[]';
    }

    var pros = JSON.parse(strPros);
    if (typeof pros === 'undefined') {
        pros = [];
    }
    for (var i = 0; i < pros.length; i++) {
        if (pros[i].Id === id) {
            pros.splice(i, 1);
        }
    }
    $.cookie(CK_PROC_CART, JSON.stringify(pros))
    loadCartList();
}

function formatMoney(amount, decimalCount = 2, decimal = ".", thousands = ",") {
    try {
        decimalCount = Math.abs(decimalCount);
        decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

        const negativeSign = amount < 0 ? "-" : "";

        let i = parseInt(amount = Math.abs(Number(amount) || 0).toFixed(decimalCount)).toString();
        let j = (i.length > 3) ? i.length % 3 : 0;

        return negativeSign + (j ? i.substr(0, j) + thousands : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands) + (decimalCount ? decimal + Math.abs(amount - i).toFixed(decimalCount).slice(2) : "");
    } catch (e) {
        console.log(e)
    }
}

function formatNumber(nStr, decSeperate, groupSeperate) {
    nStr += '';
    x = nStr.split(decSeperate);
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + groupSeperate + '$2');
    }
    return x1 + x2;
}

$(document).ready(function () {
    loadCart();
});

//dimiss alert
$(document).ready(function () {
    setTimeout(function () {
        $('.alert_page').empty();
    }, 5000)
});
