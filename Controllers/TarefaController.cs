using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPIFeitoComMVCSolo.Context;
using DesafioAPIFeitoComMVCSolo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPIFeitoComMVCSolo.Controllers
{
    public class TarefaController : Controller
    {
        private readonly TarefaContext _context;



    public TarefaController(TarefaContext context)      
    {
         _context = context;
    }

    public IActionResult Index()
    {
            var tarefas = _context.Tarefas.ToList();
            return View(tarefas);
    }

    public IActionResult Edit(int id)
    {
            var tarefa = _context.Tarefas.Find(id);
            if(tarefa == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
    }

     [HttpPost]
      public IActionResult Edit(Tarefa tarefa)
      {
        if (tarefa==null)
        {
                return BadRequest("Tarefa inválido");
        }

            var tarefaBd = _context.Tarefas.Find(tarefa.Id);

            if(tarefaBd!= null)
            {
                tarefaBd.Titulo = tarefa.Titulo;
                tarefaBd.Descricao = tarefa.Descricao;
                tarefaBd.Data = tarefa.Data;
                tarefaBd.Status = tarefa.Status;

                _context.Tarefas.Update(tarefaBd);
                _context.SaveChanges();
                return RedirectToAction();
            }
      
            else
            {
                return NotFound("Tarefa Não encontrada");
            }
      }
      public IActionResult Delete(int id)
      {
            var tarefa = _context.Tarefas.Find(id);
            if(tarefa == null)
            {
                RedirectToAction(nameof(Index));
            }
            return View(tarefa); 
      }

     [HttpPost]
      public IActionResult Delete(Tarefa tarefa)
      {
            var tarefaBd = _context.Tarefas.Find(tarefa.Id);
            if(tarefaBd == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Tarefas.Remove(tarefaBd);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
      }

      public IActionResult Create()
      {
            return View();
      }

      [HttpPost]
      public IActionResult Create(Tarefa tarefa)
      {
            if (ModelState.IsValid)
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);

      }

     public IActionResult GetByTitle(string title)
     {
           // var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(title)).ToList();
           var tarefa = _context.Tarefas.Find(title);


            return View(tarefa);
     }

     public IActionResult GetByDate(DateTime date)
     {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == date.Date).ToList();
            return View(tarefa);
     }

       public IActionResult GetById(int id)
     {
            var tarefa = _context.Tarefas.Find(id);

            return View(tarefa);
     }


        
    }
}