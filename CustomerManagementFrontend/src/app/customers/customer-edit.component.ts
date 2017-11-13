import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {IMyDpOptions, IMyDateModel} from 'mydatepicker/src/my-date-picker/index';

import { DataService } from '../core/data.service';
import { ICustomer, IGender, IColor } from '../shared/interfaces';

@Component({
  selector: 'customer-edit',
  templateUrl: './customer-edit.component.html'
})
export class CustomerEditComponent implements OnInit {

  customer: ICustomer = {
    LastName: '',
    FirstName: '',
    Gender: 0,
    FavoriteColor: 0,
    DateOfBirth: new Date()
  };

  colors: IColor[] = []
  dobmodel:any = { };
  errorMessage: string;
  deleteMessageEnabled: boolean;
  operationText: string = 'Insert';


  constructor(private router: Router,
    private route: ActivatedRoute,
    private dataService: DataService) { }

  ngOnInit() {
    let id = this.route.snapshot.params['id'];
    if (id !== '0') {
      this.operationText = 'Update';
      this.getCustomer(id);
    }

    this.getColors()
  }

  getCustomer(id: string) {
    this.dataService.getCustomer(id)
      .subscribe((customer: ICustomer) => {
        this.customer = customer;
        this.dobmodel = {date:customer.DateOfBirth}
      },
      (err: any) => console.log(err));
  }

  getColors() {
    this.dataService.getColors()
      .subscribe((colors: IColor[]) => {
        this.colors = colors;
      },
      (err: any) => console.log(err));
  }

  submit() {

    if (this.customer.Id) {
      this.customer.DateOfBirth = new Date(this.dobmodel.formatted?this.dobmodel.formatted:this.dobmodel.date)
      this.dataService.updateCustomer(this.customer)
        .subscribe((customer: ICustomer) => {
          if (customer) {
            this.router.navigate(['/customers']);
          } else {
            this.errorMessage = 'Unable to save customer';
          }
        },
        (err: any) => console.log(err));

    } else {
      this.customer.DateOfBirth = new Date(this.dobmodel.formatted)
      this.dataService.insertCustomer(this.customer)
        .subscribe((customer: ICustomer) => {
          if (customer) {
            this.router.navigate(['/customers']);
          }
          else {
            this.errorMessage = 'Unable to add customer';
          }
        },
        (err: any) => console.log(err));

    }
  }

  cancel(event: Event) {
    event.preventDefault();
    this.router.navigate(['/']);
  }

  delete(event: Event) {
    event.preventDefault();
    this.dataService.deleteCustomer(this.customer.Id)
      .subscribe((status: boolean) => {
        if (status) {
          this.router.navigate(['/customers']);
        }
        else {
          this.errorMessage = 'Unable to delete customer';
        }
      },
      (err) => console.log(err));
  }

  // onDateChanged(event:IMyDateModel)
  // {
  //   this.customer.DateOfBirth = new Date(event.formatted)
  // }
}