import { Component, Input, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "quiz",
    templateUrl : "./quiz.component.html",
    styleUrls : ['./quiz.component.css']
})

export class QuizComponent {
  quiz: Quiz;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router, private http: HttpClient,
  @Inject('BASE_URL') private baseUrl : string) {
    //create an empty object from the quiz interface
    this.quiz = <Quiz>{};

    //get the id of the current activated route once the user clicks the button
    //this allows us to use this information to determine whether or not we can go to
    //the next page containing the quiz information.
    var id = +this.activatedRoute.snapshot.params["id"];
    console.log(id);

    if (id) {
      var url = this.baseUrl + 'api/quiz/' + id;

      this.http.get<Quiz>(url).subscribe(result => {
        this.quiz = result;
      });
    } else {
      console.log("invalide id : routing back to home ...");
      this.router.navigate(["home"]);
    }
  }
}
