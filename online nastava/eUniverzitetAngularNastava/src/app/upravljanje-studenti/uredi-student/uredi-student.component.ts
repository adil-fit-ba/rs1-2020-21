import {Component, Input, OnInit} from '@angular/core';
import {StudentRow} from "./student-prikaz-vm";
import {MyConfig} from "../../myconfig";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-uredi-student',
  templateUrl: './uredi-student.component.html',
  styleUrls: ['./uredi-student.component.css']
})
export class UrediStudentComponent implements OnInit {
  @Input()
  student: StudentRow=null;

  constructor(private http:HttpClient) {
  }
  ngOnInit(): void {
  }

  snimi() {
    this.http.post(MyConfig.adresaServer + "/Student2/SnimiImePrezime", this.student, MyConfig.httpOpcije).subscribe((result)=>{
      //alert("uspjesno snimljeno");
      this.student = null;
    });
  }
}
