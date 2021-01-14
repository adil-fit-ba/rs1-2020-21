import {Component, OnInit} from '@angular/core';
import {StudentPrikazVM, StudentRow} from './student-prikaz-vm';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {MyConfig} from './MyConfig';
import {PagingInfo} from './mypaging/pagingInfo';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{

  studentPrikaz:StudentPrikazVM=null;
  trazi: string='';
  editStudent: StudentRow;


  pagingInfo:PagingInfo;

  constructor(private http:HttpClient)
  {
    let x = this;
    this.pagingInfo = new class extends PagingInfo{
      getTotalItems(): number {
        return x.studentPrikaz==null?0:x.studentPrikaz.total;
      }

      preuzmiPodatke() {
        x.preuzmiPodatke();
      }
    };
  }

  preuzmiPodatke() {
    this.http.get<StudentPrikazVM>(MyConfig.adresaServer+ "/Student2/Index?currentPage="+this.pagingInfo.currentPage+"&itemsPerPage="+this.pagingInfo.itemsPerPage).subscribe((result)=>{
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



  getTotalItems(): number {
    if (this.studentPrikaz == null)
      return 0;

    return this.studentPrikaz.total;
  }


}



