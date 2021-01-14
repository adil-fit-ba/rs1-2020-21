import {Component, Input, OnInit} from '@angular/core';
import {StudentRow} from '../../student-prikaz-vm';
import {MyConfig} from '../../MyConfig';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {
  @Input() editStudent: StudentRow;


  ngOnInit(): void {
  }

  constructor(private http:HttpClient) {
  }


  snimi() {
    this.http.post(MyConfig.adresaServer + "/Student2/SnimiImePrezime", this.editStudent, MyConfig.httpOpcije).subscribe((result)=>{
      alert("uspjesno snimljeno");
    });
  }

}
