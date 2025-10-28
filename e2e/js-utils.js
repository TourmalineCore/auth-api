function fn() {
  function decodeString(string) {
    var Bytes = Java.type('java.util.Base64');
    var decodedBytes = Bytes.getDecoder().decode(string);
    
    return new java.lang.String(decodedBytes);
  }
  
  return {
    getEnvVariable: function (variable) {
      var System = Java.type('java.lang.System');

      return System.getenv(variable);
    },

    getEmployeeIdFromToken: function (tokenValue) {
      var decodedString;

      if (tokenValue.includes('.')) {
        var payload = tokenValue.split('.')[1];

        decodedString = decodeString(payload);
      } else {
        decodedString = decodeString(tokenValue);
      }

      var tokenData = JSON.parse(decodedString);

      return tokenData.employeeId;
    }
  }
}