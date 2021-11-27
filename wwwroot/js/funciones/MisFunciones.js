function CargarCarrito() {
    var lista = $("#listCarrito");
    lista.find("li").remove();
    
    $.ajax({
        type: "GET",
        url: "/Carrito/listar",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var cant = (Object.keys(data).length);
            var total = 0;
            $("#txtCantCarrito").html(cant);
            console.log(data);
            if (data.length == 0) {
                lista.append("<div class='cart-title'>");
                lista.append("<span> Su carrito está vacío </span>");
                lista.append("</div>");
            }
            else {
            
            if (cant > 0) {
               // console.log(data);
                $.each(data, function (i, item) {
                    lista.append("<li>");
                    
                    lista.append("<div class='cart-img'>");
                    lista.append("<h5 style='font-size:14px;'><a href='#'><img style='width:40px;' src='~/img/PRODUCTOS/" + item.imagen + "' /></a>");
                    lista.append("<a href='#'>" + item.nom + "</a></h5>");

                    lista.append("</div>");

                    lista.append("<div class='cart-title'>");
                    lista.append("<span class='label label-color label-sm'> Subtotal s/" + item.precio*item.cantidad+"</span>");
                    lista.append("</div>");

                    lista.append("<div class='clearfix'></div>");

                    lista.append("</li>");
                    total = total + (item.precio * item.cantidad);

                });
            }
            //-------------Total-------------
            lista.append("<li>");
            lista.append("</div>");
            lista.append("<div class='cart-title'>");
            lista.append("<span class='label label-color label-sm'> Total Importe s/" + total + "</span>");
            lista.append("</div>");

            lista.append("</li>");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error Imposible cargar datos del carrito.');
        }
    });
}

