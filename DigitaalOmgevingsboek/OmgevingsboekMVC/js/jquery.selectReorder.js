/*
* jQuery selectReorder
* By: Federico Giust
* Version 0.1
* 
* Copyright 2013 Federico Giust
*/
(function ($) {

    $.fn.extend({

        selectReorder: function(options) {
                        
            var settings = $.extend({
                // These are the defaults.
                labelbtnUp: "Up",
                labelbtnDown: "Down",
                btnPosition: "right",
                wrapInDiv: true,
                jQueryUI: false
            }, options );
            
            return this.each(function(){
                element = $(this).attr('id');

                if( settings.wrapInDiv ){
                    $(this).wrap('<div id="' + element + '-selectReorder-container" class="selectReorder-container"></div>');
                }
                
                $(this).addClass('selectReorder');

                if( settings.btnPosition === 'right' || settings.btnPosition === null ){
                    $(this).after('<div id="' + element + '-btn-container" class="selectReorder-btn-container"><input type="button" id="' + element + 'btn-up" class="selectReorder-btn-up" value="' + settings.labelbtnUp + '" /><br /><input type="button" id="' + element + 'btn-down" class="selectReorder-btn-down" value="' + settings.labelbtnDown + '" /></div>');
                }else if( settings.btnPosition === 'left' ){
                    $(this).before('<div id="' + element + '-btn-container" class="selectReorder-btn-container"><input type="button" id="' + element + 'btn-up" class="selectReorder-btn-up" value="' + settings.labelbtnUp + '" /><br /><input type="button" id="' + element + 'btn-down" class="selectReorder-btn-down" value="' + settings.labelbtnDown + '" /></div>');
                }

                if(settings.jQueryUI){
                    $('#' + element + '-btn-container #btn-up, #' + element + '-btn-container #btn-down').button();
                }

                $('#' + element + 'btn-up').bind('click', function() {
                    console.log('#' + element);
                    $('#' + element + ' option:selected').each( function() {
                        var Pos = $('#' + element + ' option').index(this);
                        var newPos = $('#' + element + ' option').index(this) - 1;
                        var value = $('#' + element + ' option').eq(Pos).val();
                        var newValue = $('#' + element + ' option').eq(newPos).val();
                        
                        if (newPos > -1) {
             
                            $('#' + element + ' option').eq(newPos).before("<option value='"+value+"' selected='selected'>"+$(this).text()+"</option>");
             
                            $(this).remove();
                            $('#' + element + ' option').eq(Pos).val(newValue);

                        }
                    });
                });

                $('#' + element + 'btn-down').bind('click', function() {
                    var countOptions = $('#' + element + ' option').size();
                    $('#' + element + ' option:selected').each( function() {
                        var Pos = $('#' + element + ' option').index(this);
                        var newPos = $('#' + element + ' option').index(this) + 1;
                        var value = $('#' + element + ' option').eq(Pos).val();
                        var newValue = $('#' + element + ' option').eq(newPos).val();

                        if (newPos < countOptions) {
                            $('#' + element + ' option').eq(newPos).after("<option value='"+value+"' selected='selected'>"+$(this).text()+"</option>");
             
                            $(this).remove();
                            $('#' + element + ' option').eq(Pos).val(newValue);
                  
                        }
                    });
                });                

            });

            
        }

    })

})(jQuery);
