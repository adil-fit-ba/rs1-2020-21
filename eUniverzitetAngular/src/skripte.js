function hello() {
  alert('Hello!!!');
}



function generisiPreview(input) {
  var file = input.files[0];

  if (file)
  {
    var reader = new FileReader();

    reader.onload = function ()
    {
      document.getElementById("previewImg").setAttribute("src", reader.result+"");
    }

    reader.readAsDataURL(file);
  }
}
