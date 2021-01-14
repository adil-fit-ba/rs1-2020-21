import {HttpHeaders} from '@angular/common/http';

export class MyConfig{
  static httpOpcije= {
    headers:new HttpHeaders({
      "Content-Type": "application/json",

    })
  };

  //static adresaServer:string="https://api.p2079.app.fit.ba";
  static adresaServer:string="http://localhost:4100";

}


