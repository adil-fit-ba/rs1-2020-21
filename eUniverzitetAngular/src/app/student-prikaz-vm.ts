import {Component} from '@angular/core';
import {Observable} from 'rxjs';
import { HttpClient } from '@angular/common/http';

export class StudentRow {
 public id: number;
  public  brojIndeksa: string;
  public ime: string;
  public  prezime: string;
  public  opstinaPrebivalista: string;
  public  opstinaRodjenja: string;
  public  email: string;
}

export class StudentPrikazVM {
  public  studenti: StudentRow[];
  public  q?: any;



}
