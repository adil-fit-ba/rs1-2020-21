import {Component, OnInit} from '@angular/core';
import {StudentPrikazVM, StudentRow} from './student-prikaz-vm';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {MyConfig} from './MyConfig';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  ngOnInit(): void {
    //poziva se prilikom generisanja UI komponenti
  }

  studentPrikaz:StudentPrikazVM=null;
  trazi: string='';
  editStudent: StudentRow;
  currentPage: number;

  constructor(private http:HttpClient) {
  }
  snimi() {
    this.http.post(MyConfig.adresaServer + "/Student2/SnimiImePrezime", this.editStudent, MyConfig.httpOpcije).subscribe((result)=>{
      alert("uspjesno snimljeno");
    });
  }
  preuzmiPodatke() {

    this.http.get<StudentPrikazVM>(MyConfig.adresaServer+ "/Student2/Index").subscribe((result)=>{
      this.studentPrikaz = result;

    });
  }

  obrisi(s: StudentRow) {
    this.http.get(MyConfig.adresaServer+ "/Student2/Obrisi?StudentID="+s.id).subscribe((result)=>{
      //  alert('obrisano ' + s.ime);
      let indexOf = this.studentPrikaz.studenti.indexOf(s);
      this.studentPrikaz.studenti.splice(indexOf, 1);
    });
  }

  uredi(s: StudentRow) {
    this.editStudent = s
  }

  getStudenti():StudentRow[] {
    return this.studentPrikaz.studenti.filter(s=>s.ime.startsWith(this.trazi));
  }


  changePage($event: number) {

  }
}

