import { Component, Input } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
@Component({
    selector: "quiz",
    templateUrl : "./quiz.component.html",
    styleUrls : ['./quiz.component.css']
})

export class QuizComponent {
  quiz: Quiz;

  constructor(private activatedRoute: ActivatedRoute, private router: Router) {
    //create an empty object from the quiz interface
    this.quiz = <Quiz>{};

    var id = +this.activatedRoute.snapshot.params["id"];
    console.log(id);

    if (id) {
      //TO-DO : load the quiz server-side API
    } else {
      console.log("invalide id : routing back to home ...");
      this.router.navigate(["home"]);
    }
  }
}
