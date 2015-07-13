$(document).ready(function(){
    $('a#copy-description').zclip({
        path:'Content/ZClip/ZeroClipboard.swf',
        copy: $('#Promocodespan').text(),
        clickAfter:true
    });
    // The link with ID "copy-description" will copy
    // the text of the paragraph with ID "description"
    
});
