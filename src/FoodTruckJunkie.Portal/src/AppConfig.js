export default class AppConfig
{
    static BaseAPIUrl() {
        if(process.env.REACT_APP_ENV == 'local')
            return  'http://localhost:8089';
        else if(process.env.REACT_APP_ENV == 'prod') 
            return 'https://beta-api.azureworkbench.com';
        else
            return  'http://localhost:8089';
     }
}