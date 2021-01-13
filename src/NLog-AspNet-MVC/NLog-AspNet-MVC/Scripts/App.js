(function() {
    function prettyPrint($pre) {
        if ($pre.hasClass("prettyprinted")) {
            // already pretty printed
            return;
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