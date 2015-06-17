$(function() {
    var connection = $.hubConnection();
    var simpleHub = connection.createHubProxy('simpleHub');
    simpleHub.on('notifySimple', function(message, detail) {
        alert(message + ': ' + detail);
    });
    connection.start(function() {
      $('a').click(function() {
        simpleHub.invoke('sendSimple', 'foo', 'bar');
      }); 
    });
});