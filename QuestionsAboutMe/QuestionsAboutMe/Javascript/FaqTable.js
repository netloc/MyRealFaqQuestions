var synth = window.speechSynthesis;
faqToRead = "";
answerToRead = "";

DoFaqLookup = function (val) {
    $table = $("<table></table>");

    $("#divFAQList").empty();
    var isFirst = true;
    for (i = 0; i < myArray.length; i++) {
        if (i % 2 == 0) { // Even only to be checked
            if (myArray[i].toLowerCase().includes(val.toLowerCase()) && val.trim()) {

                var $questionRow = $("<tr></tr>");
                $questionRow.append($("<td></td>").html(myArray[i]));
                $table.append($questionRow);

                var $answerRow = $("<tr></tr>");
                $answerRow.append($("<td></td>").html(myArray[i + 1]));
                $table.append($answerRow);

                if (isFirst) {
                    isFirst = false;
                    faqToRead = myArray[i];
                    answerToRead = myArray[i + 1];
                }
            }
        }
    }

    $table.appendTo($("#divFAQList"));
};

PopulateAllFaq = function () {
    $table = $("<table></table>");

    $("#divFAQList").empty();

    for (i = 0; i < myArray.length; i++) {
        var $questionRow = $("<tr></tr>");
        $questionRow.append($("<td></td>").html(myArray[i]));
        $table.append($questionRow);
    }

    $table.appendTo($("#divFAQList"));
};

ReadMyFAQ = function () {

    var synth = window.speechSynthesis;

    var UtterFAQ = new SpeechSynthesisUtterance(faqToRead);
    var utterAnswer = new SpeechSynthesisUtterance(answerToRead);
    synth.speak(UtterFAQ);

    setTimeout(function () {
        synth.speak(utterAnswer);
    }, 3000);
};

