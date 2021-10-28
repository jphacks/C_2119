from django.shortcuts import render
from django.http import HttpResponse
from django.views.generic.base import TemplateView

# Create your views here.
def index(request):
    return HttpResponse("Hello, world. You're at the polls index.")

class TopView(TemplateView):
    template_name = 'pro/top.html'