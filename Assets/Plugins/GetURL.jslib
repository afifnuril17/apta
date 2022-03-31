mergeInto(LibraryManager.library, {
   
   GetURLFromPage: function () {
        var returnStr = window.top.location.href;
        var bufferSize = lengthBytesUTF8(returnStr) + 1
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;
   },
 
   GetQueryParam: function(paramId) {
        var urlParams = new URLSearchParams(location.search);
        var param = urlParams.get(Pointer_stringify(paramId));
        var bufferSize = lengthBytesUTF8(param) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(param, buffer, bufferSize);
        return buffer;
   },

   FinishGame: function (str) {
        window.location.href=Pointer_stringify(str);
	     localStorage.removeItem("from");
        localStorage.removeItem("rendered");
   },
   
   OnGameStarted: function () {
        window.gameIsRunning = true;
   },

   AddRendered: function (str){
        localStorage.setItem("rendered", true);
   },
});