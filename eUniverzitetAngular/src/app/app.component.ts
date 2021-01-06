import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {StudentPrikazVM, StudentRow} from './student-prikaz-vm';
import {StudentDodajVM} from './student-dodaj-vm';
import {Myconfig} from './myconfig';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent  {

  title:string = 'euniverzitetApp';
  prikazVM: StudentPrikazVM =null;
  trazi: string="";
  editStudent: StudentDodajVM=null;

  constructor(private http: HttpClient) {
  }



  preuzmiPodatke(){
    this.http.get<StudentPrikazVM>(Myconfig.webAppUrl+'/Student2/Index').subscribe((a) => {
      this.prikazVM = a;
    });

  }

  uredi(d: StudentRow) {
    this.http.get<StudentDodajVM>(Myconfig.webAppUrl+'/Student2/Uredi?StudentID=' + d.id).subscribe((a) => {
      this.editStudent = a;
    });
  }

  obrisi(d: StudentRow) {
    let indexOf:number = this.prikazVM.studenti.indexOf(d);
    this.prikazVM.studenti.splice(indexOf,1);
  }

  getStudenti():Array<StudentRow> {
    return this.prikazVM.studenti.filter(x => x.opstinaRodjenja.startsWith(this.trazi));
  }

  getSlikaStudentaCurrent() {
    return Myconfig.webAppUrl+ "/uploads/" +  this.editStudent.slikaStudentaCurrent;
  }

  httpOptions = {
   // headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    headers: new HttpHeaders({ 'Content-Type': 'multipart/form-data' })
  };

  snimi()
  {
    this.http.post(Myconfig.webAppUrl + "/Student/Snimi", this.editStudent, this.httpOptions).subscribe(d=>{
        this.editStudent=null;
    });
  }

  testiraj() {
    this.prikazVM.studenti[0].prezime="Tanovic";
  }


  generisiPreview() {
    // @ts-ignore
    let file = document.getElementById("fileSlika").files[0];

    if (file)
    {
      var reader = new FileReader();

      //let student = this.editStudent;
      reader.onload = function ()
      {
        let s = reader.result.toString();
        document.getElementById("previewImg").setAttribute("src", s);
       // student.slikaStudentaNew = s;
      }

      reader.readAsDataURL(file);
    }
  }
}

