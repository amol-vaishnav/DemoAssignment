import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegistrationService } from '../Services/registration.service'
import { MustMatch } from '../helper/mustmatch.validator'
@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {
  registerForm: FormGroup;
  loading = false;
  submitted = false;
  selectedFile: File;
  imagename:string;
  @ViewChild('fileInput') userPhoto: any;
  constructor( private formBuilder: FormBuilder,private registration:RegistrationService,
    private router: Router) { 
      this.InitializeControls();
    }

  ngOnInit() {
    
}
onFileChanged(event) {
  this.selectedFile = event.target.files[0]
  this.imagename=event.target.files[0].name;
  console.log(this.imagename);
}
InitializeControls()
{
  this.registerForm = this.formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    address:['',Validators.required],
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword:['',[Validators.required]]
},
{validator: MustMatch('password', 'confirmPassword')});
}
onSubmit() {
  this.submitted = true;
 // this.MustMatch('password', 'confirmPassword');
 if (this.registerForm.invalid) {
  return;
}
let formData:FormData = new FormData(); 
formData.append('firstName',this.registerForm.controls.firstName.value); 
formData.append('lastName',this.registerForm.controls.lastName.value); 
formData.append('address',this.registerForm.controls.address.value); 
formData.append('username',this.registerForm.controls.username.value); 
formData.append('password',this.registerForm.controls.password.value);
formData.append('profilePhoto',this.selectedFile); 

this.registration.insert(formData)
            .pipe(first())
            .subscribe(
                data => {
                    alert('User Register Succesfully');
                   this.InitializeControls();
                   this.imagename=null;
                    this.selectedFile=null;
                    this.userPhoto.nativeElement.value = null;
                    //this.router.navigate(['/']);
                   // counter = counter+1;
                },
                error => {
                  alert(JSON.stringify(error));
                  this.loading = false;
                });

}
onUpload()
{

}
// convenience getter for easy access to form fields
get f() { return this.registerForm.controls; }

 

passwordcheck(formGroup: FormGroup) {
  const { value: password } = formGroup.get('password');
  const { value: confirmPassword } = formGroup.get('confirmPassword');
  return password === confirmPassword ? null : { passwordNotMatch: true };
  }

}

