Feature: CORS Settings
    # https://github.com/karatelabs/karate/issues/1191
    # https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

  Background:
    * header Content-Type = 'application/json'
    
  Scenario: Verify API CORS settings

    * def jsUtils = read('./js-utils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authLogin = jsUtils().getEnvVariable('AUTH_FIRST_TENANT_LOGIN_WITH_ALL_PERMISSIONS')
    * def authPassword = jsUtils().getEnvVariable('AUTH_FIRST_TENANT_PASSWORD_WITH_ALL_PERMISSIONS')
    * def corsAllowedOrigins = jsUtils().getEnvVariable('CORS_ALLOWED_ORIGINS')

    # Send CORS preflight OPTIONS request like UI does
    Given url authApiRootUrl
    And path '/login'
    And request
    """
    {
        "login": "#(authLogin)",
        "password": "#(authPassword)"
    }
    """
    And header Origin = corsAllowedOrigins
    And header Access-Control-Request-Method = 'POST'
    When method OPTIONS
    Then status 204
    And match responseHeaders["Access-Control-Allow-Origin"] == ["#(corsAllowedOrigins)"]
    And match responseHeaders["Access-Control-Allow-Methods"] == ["GET,POST,DELETE"]
    And match responseHeaders["Access-Control-Allow-Headers"] == ["Authorization,Content-Type"]