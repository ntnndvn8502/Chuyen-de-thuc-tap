let editor;
const a = document.getElementById("RtrDesc");

ClassicEditor.create(document.querySelector("#editor"))
    .then((newEditor) => {
        editor = newEditor;
    })
    .catch((error) => {
        console.error(error);
    });

// Assuming there is a <button id="submit">Submit</button> in your application.
document.querySelector("#submit").addEventListener("click", () => {
    const editorData = editor.getData();
    a.value = editorData;

    // ...
});