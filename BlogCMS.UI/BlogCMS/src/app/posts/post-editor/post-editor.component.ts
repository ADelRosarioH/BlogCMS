import { Component, Input, OnInit } from '@angular/core';
import { EditorOption } from 'angular-markdown-editor';
import { MarkdownService } from 'ngx-markdown';
import { Post } from 'src/app/services/post.service';

@Component({
  selector: 'app-post-editor',
  templateUrl: './post-editor.component.html',
  styleUrls: ['./post-editor.component.scss']
})
export class PostEditorComponent implements OnInit {
  @Input() post?: Post;
  editorOptions: EditorOption;

  constructor(private markdownService: MarkdownService) {
    this.editorOptions = {
      parser: (val) => this.markdownService.parse(val.trim())
    };
  }

  ngOnInit(): void {
    
  }
}
