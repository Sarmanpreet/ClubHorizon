(function($){"use strict";if($(window).width()>767){if($('.theiaStickySidebar').length>0){$('.theiaStickySidebar').theiaStickySidebar({additionalMarginTop:30});}}
if($(window).width()<=991){var Sidemenu=function(){this.$menuItem=$('.main-nav a');};function init(){var $this=Sidemenu;$('.main-nav a').on('click',function(e){if($(this).parent().hasClass('has-submenu')){e.preventDefault();}
if(!$(this).hasClass('submenu')){$('ul',$(this).parents('ul:first')).slideUp(350);$('a',$(this).parents('ul:first')).removeClass('submenu');$(this).next('ul').slideDown(350);$(this).addClass('submenu');}else if($(this).hasClass('submenu')){$(this).removeClass('submenu');$(this).next('ul').slideUp(350);}});}
init();}
var maxLength=100;$('#review_desc').on('keyup change',function(){var length=$(this).val().length;length=maxLength-length;$('#chars').text(length);});if($('.select').length>0){$('.select').select2({minimumResultsForSearch:-1,width:'100%'});}
if($('.datetimepicker').length>0){$('.datetimepicker').datetimepicker({format:'DD/MM/YYYY',icons:{up:"fas fa-chevron-up",down:"fas fa-chevron-down",next:'fas fa-chevron-right',previous:'fas fa-chevron-left'}});}
var tooltipTriggerList=[].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList=tooltipTriggerList.map(function(tooltipTriggerEl){return new bootstrap.Tooltip(tooltipTriggerEl)})
if($('.floating').length>0){$('.floating').on('focus blur',function(e){$(this).parents('.form-focus').toggleClass('focused',(e.type==='focus'||this.value.length>0));}).trigger('blur');}
$('body').append('<div class="sidebar-overlay"></div>');$(document).on('click','#mobile_btn',function(){$('main-wrapper').toggleClass('slide-nav');$('.sidebar-overlay').toggleClass('opened');$('html').addClass('menu-opened');return false;});$(document).on('click','.sidebar-overlay',function(){$('html').removeClass('menu-opened');$(this).removeClass('opened');$('main-wrapper').removeClass('slide-nav');});$(document).on('click','#menu_close',function(){$('html').removeClass('menu-opened');$('.sidebar-overlay').removeClass('opened');$('main-wrapper').removeClass('slide-nav');});$(".hours-info").on('click','.trash',function(){$(this).closest('.hours-cont').remove();return false;});$(".add-hours").on('click',function(){var hourscontent='<div class="row form-row hours-cont">'+
'<div class="col-12 col-md-10">'+
'<div class="row form-row">'+
'<div class="col-12 col-md-6">'+
'<div class="form-group">'+
'<label>Start Time</label>'+
'<select class="form-control">'+
'<option>-</option>'+
'<option>12.00 am</option>'+
'<option>12.30 am</option>'+
'<option>1.00 am</option>'+
'<option>1.30 am</option>'+
'</select>'+
'</div>'+
'</div>'+
'<div class="col-12 col-md-6">'+
'<div class="form-group">'+
'<label>End Time</label>'+
'<select class="form-control">'+
'<option>-</option>'+
'<option>12.00 am</option>'+
'<option>12.30 am</option>'+
'<option>1.00 am</option>'+
'<option>1.30 am</option>'+
'</select>'+
'</div>'+
'</div>'+
'</div>'+
'</div>'+
'<div class="col-12 col-md-2"><label class="d-md-block d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a></div>'+
'</div>';$(".hours-info").append(hourscontent);return false;});function resizeInnerDiv(){var height=$(window).height();var header_height=$(".header").height();var footer_height=$(".footer").height();var setheight=height-header_height;var trueheight=setheight-footer_height;$(".content").css("min-height",trueheight);}
if($('.content').length>0){resizeInnerDiv();}
$(window).resize(function(){if($('.content').length>0){resizeInnerDiv();}});if($('.specialities-slider').length>0){$('.specialities-slider').slick({dots:true,autoplay:false,infinite:true,variableWidth:true,prevArrow:false,nextArrow:false});}
if($('.doctor-slider').length>0){$('.doctor-slider').slick({dots:false,autoplay:false,infinite:true,variableWidth:true,});}
if($('.features-slider').length>0){$('.features-slider').slick({dots:true,infinite:true,centerMode:true,slidesToShow:3,speed:500,variableWidth:true,arrows:false,autoplay:false,responsive:[{breakpoint:992,settings:{slidesToShow:1}}]});}
if($('.bookingrange').length>0){var start=moment().subtract(6,'days');var end=moment();function booking_range(start,end){$('.bookingrange span').html(start.format('MMMM D, YYYY')+' - '+end.format('MMMM D, YYYY'));}
$('.bookingrange').daterangepicker({startDate:start,endDate:end,ranges:{'Today':[moment(),moment()],'Yesterday':[moment().subtract(1,'days'),moment().subtract(1,'days')],'Last 7 Days':[moment().subtract(6,'days'),moment()],'Last 30 Days':[moment().subtract(29,'days'),moment()],'This Month':[moment().startOf('month'),moment().endOf('month')],'Last Month':[moment().subtract(1,'month').startOf('month'),moment().subtract(1,'month').endOf('month')]}},booking_range);booking_range(start,end);}
var chatAppTarget=$('.chat-window');(function(){if($(window).width()>991)
chatAppTarget.removeClass('chat-slide');$(document).on("click",".chat-window .chat-users-list a.media",function(){if($(window).width()<=991){chatAppTarget.addClass('chat-slide');}
return false;});$(document).on("click","#back_user_list",function(){if($(window).width()<=991){chatAppTarget.removeClass('chat-slide');}
return false;});})();$(window).on('load',function(){if($('#loader').length>0){$('#loader').delay(350).fadeOut('slow');$('body').delay(350).css({'overflow':'visible'});}})
$(document).ready(function(){$(".fa-search").click(function(){$(".togglesearch").toggle();$(".top-search").focus();});});if($('#customers-testimonials').length>0){$('#customers-testimonials').owlCarousel({nav:true,items:1,autoplay:true,loop:true,autoplayTimeout:50000,navText:["<i class='fas fa-arrow-left owl-nav-left'></i>","<i class='fas fa-arrow-right owl-nav-right ms-3'></i>"],});}
$(document).ready(function(){$(window).scroll(function(){var scroll=$(window).scrollTop();if(scroll>50){$(".header-trans").css("background","#134a56");$(".header-trans").css("top","0");$(".header-trans").css("position","fixed");$('body').addClass('map-up');}
else{$(".header-trans").css("background","transparent");$(".header-trans").css("top","auto");$(".header-trans").css("position","relative");$('body').removeClass('map-up');}});});$(window).on("load",function(){document.onkeydown=function(e){if(e.keyCode==123){return false;}
if(e.ctrlKey&&e.shiftKey&&e.keyCode=='I'.charCodeAt(0)){return false;}
if(e.ctrlKey&&e.shiftKey&&e.keyCode=='J'.charCodeAt(0)){return false;}
if(e.ctrlKey&&e.keyCode=='U'.charCodeAt(0)){return false;}
    if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) { return false; }
};
});
    document.oncontextmenu = function () { /*return false;*/ };
    $(document).mousedown(function (e) {
        if (e.button == 2) { return false; }
        return true;
    });
})(jQuery);