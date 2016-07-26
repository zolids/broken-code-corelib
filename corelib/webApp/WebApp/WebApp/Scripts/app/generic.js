
/**
 * Generic ajax method for json request
 * @return Json
 * @param mixed
 */
var $loadJsonResult = function params(url, params, callback) {

    $.ajax({
        url: url,
        data: params,
        type: 'POST',
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        traditional: true,
        success: callback,
        error: function (XHR, errStatus, errorThrown) {
            var err = JSON.parse(XHR.responseText);
            errorMessage = err.Message;
            alert(errorMessage);
        }
    });

},

/**
 * Generic ajax method for partial view request
 * @return partial view / HTML
 * @param mixed
 */
$loadPartialView = function params(url, callback, params) {

    $.ajax({
        url: url,
        data: params,
        dataType: "html",
        success: callback,
        error: function (XHR, errStatus, errorThrown) {
            console.log(XHR);
        }
    }).done(function (data) {

    });

},

// Read a page's GET URL variables and return them as an associative array.
$getUrlVars = function () {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
},

$pasteHtmlAtCaret = function (html) {
    var sel, range;
    if (window.getSelection) {
        // IE9 and non-IE
        sel = window.getSelection();
        if (sel.getRangeAt && sel.rangeCount) {
            range = sel.getRangeAt(0);
            range.deleteContents();

            // Range.createContextualFragment() would be useful here but is
            // only relatively recently standardized and is not supported in
            // some browsers (IE9, for one)
            var el = document.createElement("div");
            el.innerHTML = html;
            var frag = document.createDocumentFragment(), node, lastNode;
            while ((node = el.firstChild)) {
                lastNode = frag.appendChild(node);
            }
            range.insertNode(frag);

            // Preserve the selection
            if (lastNode) {
                range = range.cloneRange();
                range.setStartAfter(lastNode);
                range.collapse(true);
                sel.removeAllRanges();
                sel.addRange(range);
            }
        }
    } else if (document.selection && document.selection.type != "Control") {
        // IE < 9
        document.selection.createRange().pasteHTML(html);
    }
}

$generic = function () {
    "use strict";
    return {
        init: function () {
            
        }
    }
}();