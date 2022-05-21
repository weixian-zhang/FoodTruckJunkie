export default class AppConfig
{
    static BaseAPIUrl() {
        if(process.env.REACT_APP_ENV == 'local')
            return  'https://localhost:5001';
        else if(process.env.REACT_APP_ENV == 'prod') 
            return 'https://beta-api.azureworkbench.com';
        else
            return  'https://localhost:5001';
     }
}