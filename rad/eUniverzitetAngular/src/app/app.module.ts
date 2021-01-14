import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { FormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EditStudentComponent } from './studenti/edit-student/edit-student.component';
import { MypagingComponent } from '../helper/mypaging/mypaging.component';
import { StudentiComponent } from './studenti/studenti.component';
import { PostavkeComponent } from './postavke/postavke.component';
import { RouterModule } from '@angular/router';
@NgModule({
  declarations: [
    AppComponent,
    EditStudentComponent,
    MypagingComponent,
    StudentiComponent,
    PostavkeComponent,
  ],
    imports: [
        BrowserModule,
        FormsModule,
      HttpClientModule,
      RouterModule.forRoot([
        {path: 'otvori-studenti', component: StudentiComponent},
        {path: 'otvori-postavke', component: PostavkeComponent},
      ])
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
