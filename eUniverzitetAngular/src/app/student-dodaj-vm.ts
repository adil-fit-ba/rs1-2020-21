import {Component} from '@angular/core';
import {Observable} from 'rxjs';
import { HttpClient } from '@angular/common/http';

  export class Opstine {
    public  text: string;
    public  value: string;
  }

  export class StudentDodajVM {
    public  opstine: Opstine[];
    public  opstinaRodjenjaID: number;
    public  id: number;
    public  ime: string;
    public  email: string;
    public  prezime: string;
    public  opstinaPrebivalistaID: number;
    public  slikaStudentaNew?: any;
    public  slikaStudentaCurrent?: any;
  }

