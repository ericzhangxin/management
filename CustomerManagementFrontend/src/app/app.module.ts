import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MyDatePickerModule} from 'mydatepicker/src/my-date-picker/index'
import { AppComponent } from './app.component';
import { appRouting } from './app.routing';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  imports: [
    BrowserModule,
    appRouting.routes,
    CoreModule,   //Singleton objects
    SharedModule, //Shared (multi-instance) objects,
    MyDatePickerModule
  ],
  declarations: [AppComponent, appRouting.components],
  bootstrap: [AppComponent]
})
export class AppModule { }
