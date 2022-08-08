$(document).ready(function(){

    /*-------thanh search------*/
    $('#search, .fa-search').mouseenter(function(){
        $('.logo').hide();//khi bật thanh search tắt thanh logo
    });

    $('#search, .fa-search').mouseleave(function(){
        $('.logo').show();//khi tắt thanh search hiện ra logo
    });
    /*------end thanh search-----*/

    /*-------nút mune 3 gạch, khi click bào hiện ra------*/
    $('.fa-bars').click(function(){
        $('.navbar').toggle();//click vào hiện ra menu
        $(this).toggleClass('fa-times');//đặt tên biến và viết trong css để cho khi bấm có thời gian animation
    });

    $('.navbar, .navbar a').click(function(){
        $('.navbar').hide();//click vào bên ngoài là thoát khỏi menu
        $('.fa-bars').removeClass('fa-times');//thoát khỏi class có tên là fa-bars
    });
    /*--------end nút mune 3 gạch------*/
    
    /*-----------header giống giống kiểu flex--------*/
    $(window).on('scroll load',function(){
        //nếu kéo xún thanh header sẽ màu đỏ
        if($(window).scrollTop() > 20){
            $('#header').css({
                'background':'#EB4D4B',
                'box-shadow':'0 .1rem .3rem #000'
            });
        }else{//ngược lại sẽ màu đen
            $('#header').css({
                'background':'#333',
                'box-shadow':'none'
            });
        }
    });
    /*----------end header giống giống kiểu flex---------*/

    /*--------------slider chuyển trang tự động----------*/
    $('.home-slider').owlCarousel({
        loop:true,
        margin:10,
        nav:true,
        items:1,
        autoplay:true//tự động chuyển trang
    });
    /*-------------end slider-------------*/

    /*---------chuyển trang tự động của product----------*/
    $('.product-slider ').owlCarousel({//product sản phẩm
        loop:true,
        nav:true,
        items:3,//số trang cần chứa là 4 desc=4
        autoplay:true,//tự động chuyển trang
        center:true,
        responsive:{
            0:{
                items:1,
                nav:true
            },
            550:{
                items:2
            },
            1000:{
                items:3
            }
        }
    });
    /*---------end chuyển trang tự động của product----------*/

    /*---------review chuyển trang tự động-----------*/
    $('.review-slider').owlCarousel({
        loop:true,
        nav:true,
        items:1,
        autoplay:true//tự động chuyển trang
    });
    /*-----------end review------------*/ 

    /*------------giảm giá chuyển trang tự động----------*/
    $('.sale-slider').owlCarousel({//product sản phẩm
        loop:true,
        nav:true,
        items:5,//số trang cần chứa là 4 desc=4
        autoplay:true,//tự động chuyển trang
        center:true,
        responsive:{
            0:{
                items:0,
                nav:true
            },
            550:{
                items:2
            },
            1000:{
                items:3
            },
            1200:{
                items:4
            }
        }
    });
    /*------------end giảm giá----------*/

    /*------------page chuyển trang tự động----------*/
    $('.page-slider').owlCarousel({
        loop:true,
        nav:true,
        items:1,
        autoplay:true//tự động chuyển trang
    });
    /*------------page review ------------------*/



});

