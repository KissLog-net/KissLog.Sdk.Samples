(function () {
    function replacePlaceholders(html, placeholders) {
        if (!placeholders) {
            return html;
        }

        var resultHtml = html;

        for (var prop in placeholders) {
            if (placeholders.hasOwnProperty(prop)) {
                var value = placeholders[prop];
                var regex = new RegExp("{" + prop + "}", "g");
                resultHtml = resultHtml.replace(regex, value);
            }
        }

        return resultHtml;
    }

    function prettyPrint($pre, placeholders) {
        if ($pre.hasClass("prettyprinted")) {
            // already pretty printed
            return;
        }

        if (placeholders !== undefined) {
            var html = $pre.html();
            html = replacePlaceholders(html, placeholders);

            $pre.html(html);
        }

        var highlightLines = [];

        if ($pre.hasClass("linenums")) {
            var html = $pre.html().split(/\r?\n/);
            var newHtml = [];

            for (var i = 0, len = html.length; i < len; i++) {
                var line = html[i];
                if (line.indexOf("::") > -1) {
                    highlightLines.push(i);
                    line = line.replace(/::/g, "");
                }

                newHtml.push(line);
            }

            $pre.html(newHtml.join("\n"));
        }

        PR.prettyPrint(null, $pre.parent()[0]);

        for (var i = 0, len = highlightLines.length; i < len; i++) {
            $pre.find("ol.linenums").children("li").eq(highlightLines[i]).addClass("highlighted");
        }
    }


    window["App"] = {
        prettyPrint: prettyPrint
    };
}());