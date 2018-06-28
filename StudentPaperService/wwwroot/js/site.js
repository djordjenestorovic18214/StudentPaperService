function insertSubjectsForNewProfessor() {
    console.log(choosenSubjects);
    //$.post("/kategorije/pronadji/", { nizKategorija: $("#izborKategorije").val() },
    //    function (data) {
    //        $("#kategorijeTabelaBody tr").remove();
    //        var html = '';
    //        izabraneKategorije = [];
    //        for (var i = 0; i < data.length; i++) {
    //            izabraneKategorije.push(data[i].kategorijaId)
    //            html += '<tr><td><input type="hidden" name="izabraneKategorije" value= ' + data[i].kategorijaId + ' />' + (i + 1) +
    //                '</td><td>' + data[i].nazivKategorije + '</td></tr>';
    //        }
    //        console.log(html);
    //        $('#kategorijeTabelaBody').append(html);
    //    }
    );
}

let choosenSubjects = [];
function displaySubjectsModal() {
    console.log(choosenSubjects);
    $('#subjectChoosen').multiSelect('select', choosenSubjects);
    $("#subjectsModal").modal('show')