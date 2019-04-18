function GetContent(id) {
  //$("#blog").empty();
  //$("#content").html(content);
  $.ajax({
    type: "GET",
    async: false,
    url: "/Home/GetContentById",
    data: {
      "blogId": id
    },
    success: function (data) {
      $("#blog").empty();
      $("#content").html(data);
    }
  });
}